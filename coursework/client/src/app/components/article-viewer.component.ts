import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { ArticleService } from '../services/article.service';
import { AuthService } from '../services/auth.service';
     
import { Article } from '../models/article.model';

@Component({
    selector: 'article-viewer',
    templateUrl: 'views/article-viewer.html',
    styleUrls: ['styles/article-viewer.css']
})
export class ArticleViewerComponent implements OnInit { 
    private id : number;
    private userId : number;
    private article : Article;
    private status : number;

    constructor(private articleService: ArticleService, private route: ActivatedRoute, private auth: AuthService) {
        this.status = 0; // loading
        this.userId = this.auth.getUserId();
    }

    ngOnInit() {
        this.route.params.subscribe( params => {
            this.id = params['id'];
            this.articleService.get(this.id)
                               .then( this.showArticle.bind(this) )
                               .catch( this.showError.bind(this) );
        });
    }

    private delete() {
        this.articleService.delete(this.id)
                           .then( this.showError.bind(this) );
    }

    private showArticle(article: Article) {
        this.article = article;
        this.status = 1; // ok
    }

    private showError(error: Error) {
        this.status = 2; // error
    }
}