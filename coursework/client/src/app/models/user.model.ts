export class UserAdditionalInfo {
    public id : number;
    public type : string;
    public value : string;

    constructor(obj?:any) {
        this.id =    obj && obj.id     || null;
        this.type =  obj && obj.type   || null;
        this.value = obj && obj.value  || null;
    }
}

export class User {

    public id : number;
    public login : string;
    public articlesId : number[];
    public additionalInfo : UserAdditionalInfo[];

    constructor(obj?:any) {
        this.id    = obj && obj.id    || null;
        this.login = obj && obj.login || null;

        if (obj && obj.articles_id)
        {
            this.articlesId = [];
            for (let id of obj.articles_id) {
                this.articlesId.push(id);
            }
        }
        else
            this.articlesId = null;

        if (obj && obj.additional_info)
        {
            this.additionalInfo = [];
            for (let info of obj.additional_info) {
                let t = new UserAdditionalInfo(info);
                this.additionalInfo.push(t);
            }
        }
        else
            this.additionalInfo = null;
    }

}