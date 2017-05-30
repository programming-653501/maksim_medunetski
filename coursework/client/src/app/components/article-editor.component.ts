import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';

import { Article } from '../models/article.model';

import { ArticleService } from '../services/article.service';
     
@Component({
    selector: 'article-editor', 
    templateUrl: 'views/article-editor.html',
    styleUrls: []
})
export class ArticleEditorComponent { 
    
    id: number;
    status: number;
    form: FormGroup;

    constructor(private articleService: ArticleService, private route: ActivatedRoute, private fb: FormBuilder) {
        this.status = 0;
        this.form = this.fb.group({
            title: '',
            content: ''
        });
        this.route.params.subscribe( params => {
            this.id = params['id'];
            this.articleService.get(this.id)
                               .then( this.showArticle.bind(this) )
                               .catch( this.showError.bind(this) );
        });
    }

    private showArticle(article: Article) {
        this.form = this.fb.group({
            title: article.title,
            content: article.content
        });
        this.status = 1; // an article was got
    }

    private showError(error: Error) {
        this.status = 2; // an article wasn't got
    }

    edit() {
        this.status = 0; // loading
        let data = this.form.value;
        data.id = this.id;
        let article = new Article(data);
        this.articleService.edit(article)
                           .then(() => { this.status = 3; })
                           .catch(() => { this.status = 4; });
    }
}