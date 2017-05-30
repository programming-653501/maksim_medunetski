export class Article {

    public id : number;
    public title : string;
    public content : string;
    public userId : number;

    constructor(obj:any) {
        this.id = obj.id;
        this.title = obj.title;
        this.content = obj.content;
        this.userId = obj.user_id;
    }

}