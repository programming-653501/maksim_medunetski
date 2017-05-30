import { Injectable } from '@angular/core';
import { Response } from '@angular/http';

import { RequestService } from './request.service';

import { User, UserAdditionalInfo } from '../models/user.model';
import { Config } from '../config';

@Injectable()
export class UserService {

    constructor(private req: RequestService) {
        
    }

    getUserByLogin(login: string) : Promise<User> {
        let data = {
            login: login
        };
        return this.req.get(data, Config.UserPath)
                       .map( (resp:Response) => { 
                           let data = resp.json();
                           return new User(data);
                        } )
                       .toPromise();
    }

    getUser(id: number) : Promise<User> {
        let data = {
            id: id
        };
        return this.req.get(data, Config.UserPath)
                       .map( (resp:Response) => { 
                           let data = resp.json();
                           return new User(data);
                        } )
                       .toPromise();
    }

    addAdditionalInfo(info: UserAdditionalInfo) {
        return this.req.post(info, Config.UserPath)
                       .toPromise();
    }

    updateAdditionalInfo(info: UserAdditionalInfo) {
        return this.req.put(info, Config.UserPath)
                       .toPromise();
    }

    deleteAdditionalInfo(id: number) {
        return this.req.delete({ id: id }, Config.UserPath)
                       .toPromise();
    }
}