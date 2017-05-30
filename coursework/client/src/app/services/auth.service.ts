import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Response, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/toPromise';

import { RequestService } from './request.service';
import { StorageService } from './storage.service';
import { Config } from '../config';

@Injectable()
export class AuthService {

    constructor(private req: RequestService, private storage: StorageService) {
        
    }

    register(login: string, password: string) : Promise<any> {
        let regData = {
            login: login,
            password: password
        };
        return this.req.post(regData, Config.RegisterPath)
                       .toPromise();
    }

    login(login: string, password: string) : Promise<any> {
        let loginData = {
            login: login,
            password: password
        };
        return this.req.post(loginData, Config.TokenPath)
                       .map( (resp:Response) => resp.json() )
                       .toPromise()
                       .then( (data) => {
                            this.storage.set(Config.TokenStorageKey, data.token);
                            this.storage.set(Config.IdStorageKey, data.user_id);
                       });
    }

    getUserId() : number|null {
        let ans = this.storage.get(Config.IdStorageKey);
        if (ans === null)
            return null;
        return parseInt( this.storage.get(Config.IdStorageKey) );
    }

    logout() {
        this.storage.delete(Config.IdStorageKey);
        this.storage.delete(Config.TokenStorageKey);
    }
}