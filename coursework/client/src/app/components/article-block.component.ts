import { Component, OnInit } from '@angular/core';

import { ArticleService } from '../services/article.service';
     
import { Article } from '../models/article.model';

@Component({
    selector: 'article-block',
    templateUrl: 'views/article-block.html',
    styleUrls: ['styles/article-block.css'],
    inputs: ['id']
})
export class ArticleBlockComponent implements OnInit { 
    id : number;
    private article : Article;
    private status : number;

    constructor(private articleService: ArticleService) {
        this.status = 0; // loading
    }

    ngOnInit() {
        this.articleService.get(this.id)
                           .then( this.showArticle.bind(this) )
                           .catch( this.showError.bind(this) );
    }

    private showArticle(article: Article) {
        this.article = article;
        this.status = 1; // ok
    }

    private showError(error: Error) {
        this.status = 2; // error
    }
}