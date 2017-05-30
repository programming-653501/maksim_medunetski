import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { UserService } from '../services/user.service';
import { AuthService } from '../services/auth.service';

import { User, UserAdditionalInfo } from '../models/user.model';

@Component({
    selector: 'user-profile',
    templateUrl: 'views/user-profile.html',
    styleUrls: ['styles/user-profile.css']
})
export class UserProfileComponent implements OnInit { 
    private status: number;
    private imageUrl: string = '/images/image.png';
    private userName: string;
    private info: UserAdditionalInfo[] = [];
    private articlesId: any;

    constructor(private route: ActivatedRoute, 
                private router: Router, 
                private userService: UserService,
                private auth: AuthService) {
        this.status = 0; // loading
    }

    ngOnInit() {
        this.route.params.subscribe( params => {
            let login = params['login'];
            if (typeof login === "undefined")
            {
                this.processUndefined();
                return;
            }
            this.userService.getUserByLogin(login)
                            .then(this.showUser.bind(this))
                            .catch(this.showError.bind(this));
        });
    }

    private processUndefined() {
        let id = this.auth.getUserId();
        if (id === null)
            this.router.navigate(['','auth']);
        this.userService.getUser(id)
                        .then( this.showUser.bind(this) )
                        .catch( this.showError.bind(this) );
    }

    private showUser(user: User) {
        user.articlesId.reverse();
        this.userName = user.login;
        this.articlesId = user.articlesId;
        for (let info of user.additionalInfo)
        {
            switch (info.type)
            {
                case 'avatar':
                    this.imageUrl = info.value;
                    break;
                case 'name':
                    this.userName = info.value;
                    break;
                default:
                    this.info.push(info);
            }
        }
        this.status = 1; // ok
    }

    private showError() {
        this.status = 2;
    }
}