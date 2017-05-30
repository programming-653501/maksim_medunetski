import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule, FormBuilder }   from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';
import { HttpModule }   from '@angular/http';

import { AuthService } from './services/auth.service';
import { RequestService } from './services/request.service';
import { StorageService } from './services/storage.service';
import { UserService } from './services/user.service';
import { ArticleService } from './services/article.service';

import { AppComponent }   from './components/app.component';
import { AppHeaderComponent } from './components/app-header.component';
import { AppMenuComponent } from './components/app-menu.component';
import { DefaultTemplateComponent } from './components/default-template.component';
import { NotFoundComponent } from './components/not-found.component';
import { AuthComponent } from './components/auth.component';
import { UserProfileComponent } from './components/user-profile.component';
import { ArticlesGroupComponent } from './components/articles-group.component';
import { ArticleBlockComponent } from './components/article-block.component';
import { ArticleCreatorComponent } from './components/article-creator.component';
import { ArticleEditorComponent } from './components/article-editor.component';
import { ArticleViewerComponent } from './components/article-viewer.component';
import { SettingsComponent } from './components/settings.component';

const defaultRoutes : Routes = [
    { path: 'user', component: UserProfileComponent },
    { path: 'user/:login', component: UserProfileComponent },
    { path: 'article/add', component: ArticleCreatorComponent },
    { path: 'article/:id', children: [
        { path: '', component: ArticleViewerComponent },
        { path: 'edit', component: ArticleEditorComponent }
    ]},
    { path: 'settings', component: SettingsComponent },
    { path: '**', component: NotFoundComponent }
];

const appRoutes : Routes = [
    { path: 'auth', component: AuthComponent },
    { path: '', pathMatch:'full', redirectTo: '/user' },
    { path: '', component: DefaultTemplateComponent, children: defaultRoutes }
];

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forRoot(appRoutes),
        HttpModule
    ],
    declarations: [ 
        AppComponent,
        AppHeaderComponent,
        AppMenuComponent,
        DefaultTemplateComponent,
        NotFoundComponent,
        AuthComponent,
        UserProfileComponent,
        ArticlesGroupComponent,
        ArticleBlockComponent,
        ArticleCreatorComponent,
        ArticleEditorComponent,
        ArticleViewerComponent,
        SettingsComponent
    ],
    providers: [ 
        AuthService, 
        RequestService, 
        StorageService,
        UserService,
        ArticleService
    ],
    bootstrap:    [ AppComponent ]
})
export class AppModule { }