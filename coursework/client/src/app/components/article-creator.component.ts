import { Component } from '@angular/core';

import { Article } from '../models/article.model';

import { ArticleService } from '../services/article.service';
     
@Component({
    selector: 'article-creator', 
    templateUrl: 'views/article-creator.html',
    styleUrls: []
})
export class ArticleCreatorComponent { 
    
    status: number;

    constructor(private articleService: ArticleService) {
        this.status = 3;
    }

    add(data: any) {
        this.status = 0;
        this.articleService.add(data.title, data.content)
                           .then(() => { this.status = 1; })
                           .catch(() => { this.status = 2 });
    }
}