import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';

import { User, UserAdditionalInfo } from '../models/user.model';

import { AuthService } from '../services/auth.service';
import { UserService } from '../services/user.service';
    
@Component({
    selector: 'settings',
    templateUrl: '/views/settings.html'
})
export class SettingsComponent implements OnInit { 
    id: number;
    login: string;
    data: UserAdditionalInfo[] = [];
    status: number = 0;
    formGroups: FormGroup[] = [];

    constructor(private auth: AuthService, private userService: UserService, private fb: FormBuilder) {

    }

    ngOnInit() {
        this.id = this.auth.getUserId();
        this.userService.getUser(this.id)
                        .then( this.showUserInfo.bind(this) )
                        .catch( this.showError.bind(this) );
    }

    displayData() {
        this.formGroups = [];
        for (let info of this.data) {
            let form = this.fb.group({
                'id' : info.id,
                'type': info.type,
                'value': info.value
            });
            this.formGroups.push(form);
        }
    }

    edit(data: UserAdditionalInfo) {
        this.userService.updateAdditionalInfo(data);
    }

    delete(data: any) {
        this.userService.deleteAdditionalInfo(data.id);
        this.data = this.data.filter( (el) => {
            return el.id !== data.id;
        });
        this.displayData();
    }

    showUserInfo(user: User) {
        
        this.login = user.login;
        this.data = user.additionalInfo;
        this.displayData();
        this.status = 1;
    }

    showError(error: Error) {
        this.status = 2;
    }

    addUserInfo(info: UserAdditionalInfo) {
        this.status = 0;
        this.userService.addAdditionalInfo(info)
                        .then( this.added.bind(this, info) )
                        .catch();
    }

    added(info: UserAdditionalInfo) {
        this.data.push(info);
        this.displayData();
        this.status = 1;
    }
}