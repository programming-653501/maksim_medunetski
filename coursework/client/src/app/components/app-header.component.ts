import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router'     

import { AuthService } from '../services/auth.service';

@Component({
    selector: 'app-header',
    templateUrl: 'views/app-header.html',
    styleUrls: ['styles/app-header.css']
})
export class AppHeaderComponent implements OnInit { 
    userId : number;

    constructor(private auth: AuthService, private router: Router) {
        
    }

    ngOnInit() {
        this.userId = this.auth.getUserId();
    }

    logOut() {
        this.auth.logout();
    }

    watchUser(login: string) {
        this.router.navigate(['', 'user', login]);
    }
}