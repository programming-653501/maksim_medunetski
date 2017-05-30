import { Injectable } from '@angular/core';
import { Response } from '@angular/http';

import { RequestService } from './request.service';

import { Article } from '../models/article.model';

import { Config } from '../config';

@Injectable()
export class ArticleService {

    constructor(private req: RequestService) {
        
    }

    get(id: number) : Promise<Article> {
        let data = {
            id: id
        };
        return this.req.get(data, Config.ArcticlePath)
                       .map( (resp:Response) => { 
                           let data = resp.json();
                           return new Article(data);
                        } )
                       .toPromise();
    }

    add(title: string, content: string) {
        let data = {
            title: title,
            content: content
        };
        return this.req.post(data, Config.ArcticlePath)
                       .toPromise();
    }

    edit(article: Article) {
        let data = {
            id: article.id,
            title: article.title,
            content: article.content
        };
        return this.req.put(data, Config.ArcticlePath)
                       .toPromise();
    }

    delete(id: number) {
        let data = {
            id: id
        };
        return this.req.delete(data, Config.ArcticlePath)
                       .toPromise();
    }
}