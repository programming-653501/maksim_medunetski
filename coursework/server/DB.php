<?php

class DB
{
    private $db;

    public function __construct($host, $user, $password, $database)
    {
        $this->db = new mysqli($host, $user, $password, $database);
    }

    public function __destruct()
    {
        $this->db->close();
    }

    private function process_value($value)
    {
        $type = gettype($value);
        $val = '';
        if ($type=='string')
        {
            $t = $this->db->escape_string($value);
            $val = "'$t'";
        }
        elseif ($type=='NULL')
            $val = 'NULL';
        elseif ($type=='object')
        {
            $t = json_encode($value);
            $t = $this->db->escape_string($t);
            $val = "'$t'";
        }
        else
            $val = "$value";
        return $val;
    }

    private function array_to_object(array $array, $className)
    {
        return unserialize(sprintf(
            'O:%d:"%s"%s',
            strlen($className),
            $className,
            strstr(serialize($array), ':')
        ));
    }

    private function add_one($object)
    {
        $table = get_class($object).'s';
        $key_query = '';
        $val_query = '';
        $comma = '';
        foreach ($object as $key => $value) 
        {

            $key_query .= $comma."`$key`";
            $val = $this->process_value($value);
            $val_query .= $comma."$val";
            $comma = ',';
        }
        $q = "INSERT INTO `$table` ($key_query) VALUES ($val_query);";
        $result = $this->db->query($q);
        return $result;
    }

    private function find_one($object)
    {
        $table = get_class($object).'s';
        $and = '';
        $query = '';
        foreach ($object as $key => $value) 
        {
            if ($value==NULL)
                continue;
            $val = $this->process_value($value);
            $query .= $and."`$key` = $val";
            $and = ' AND ';
        }
        $q = "SELECT * FROM `$table` WHERE  $query";
        $result = $this->db->query($q);
        if ((!$result)||(!$result->num_rows))
            return false;
        $ret = [];
        while ($arr = $result->fetch_assoc())
        {
            $ret[] = $this->array_to_object($arr, get_class($object));
        }
        return $ret;
    }
    
    public function find($data)
    {
        if (is_array($data))
        {
            $result = [];
            foreach($data as $object)
                $result += $this->find_one($object);
            if (empty($result))
                return false;
            return $result;
        }
        return $this->find_one($data);
    }

    public function add($data)
    {
        if (is_array($data))
        {
            $result = true;
            foreach($data as $object)
                $result &= $this->add_one($object);
            return $result;
        }
        return $this->add_one($data);
    }

    private function update_one($object, $params)
    {
        $table = get_class($object).'s';
        $query = '';
        $comma = '';
        $where = '';
        $wcomma = '';
        foreach ($object as $key => $value) 
        {
            if (in_array($key, $params))
            {
                $val = $this->process_value($value);
                $where .= $wcomma."`$key`=$val";
                $wcomma = ' AND ';
                continue;
            }
            $val = $this->process_value($value);
            $query .= $comma."`$key`=$val";
            $comma = ',';
        }
        $q = "UPDATE `$table` SET $query WHERE $where";
        $result = $this->db->query($q);
        return $result;
    }

    public function update($data, $params)
    {
        if (is_array($data))
        {
            $result = true;
            foreach($data as $object)
                $result &= $this->update_one($object, $params);
            return $result;
        }
        return $this->update_one($data, $params);
    }

    private function delete_one($object)
    {
        $table = get_class($object).'s';
        $and = '';
        $query = '';
        foreach ($object as $key => $value) 
        {
            if ($value==NULL)
                continue;
            $val = $this->process_value($value);
            $query .= $and."`$key` = $val";
            $and = ' AND ';
        }
        $q = "DELETE FROM `$table` WHERE  $query";
        $result = $this->db->query($q);
        return $result;
    }

    public function delete($data)
    {
        if (is_array($data))
        {
            $result = true;
            foreach($data as $object)
                $result &= $this->delete_one($object);
            return $result;
        }
        return $this->delete_one($data);
    }
}