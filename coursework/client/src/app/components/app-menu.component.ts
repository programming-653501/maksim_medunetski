import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router'     

import { AuthService } from '../services/auth.service';

@Component({
    selector: 'app-menu',
    templateUrl: 'views/app-menu.html',
    styleUrls: ['styles/app-menu.css']
})
export class AppMenuComponent implements OnInit { 
    userId : number;
    userLogin : string;

    constructor(private auth: AuthService) {
        
    }

    ngOnInit() {
        this.userId = this.auth.getUserId();
        
    }
}