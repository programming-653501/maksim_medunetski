import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Response, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';

import { Config } from '../config';

import { StorageService } from './storage.service';

@Injectable()
export class RequestService {
    
    constructor(private http: Http, private storage: StorageService) {
        
    }

    private formFormate(data: any): string {
        let ret: string = '';
        for (let key in data) {
            let q = key + '=' + data[key] + '&';
            ret += q;
        }
        return ret;
    }

    private getHeaders(): Headers {
        let token = this.storage.get(Config.TokenStorageKey);
        return new Headers({ 
            'Authorization' : 'Bearer ' + token,
            'Content-Type' : 'application/json;charset=utf-8', 
        });
    }

    post(data: any, url: string): Observable<any> {
        const body = JSON.stringify(data);
        let headers = this.getHeaders();
        return this.http.post(url, body, { headers: headers })
                        .catch( (error:any) => { return Observable.throw(error); }); 
    }

    get(data: any, url: string): Observable<any> {
        let params = this.formFormate(data);
        let headers = this.getHeaders();
        return this.http.get(url + '?' + params, { headers: headers })
                        .catch( (error:any) => { return Observable.throw(error); }); 
    }
    
    put(data: any, url: string): Observable<any> {
        const body = JSON.stringify(data);
        let headers = this.getHeaders();
        return this.http.put(url, body, { headers: headers })
                        .catch( (error:any) => { return Observable.throw(error); }); 
    }

    delete(data: any, url: string): Observable<any> {
        let params = this.formFormate(data);
        let headers = this.getHeaders();
        return this.http.delete(url + '?' + params, { headers: headers })
                        .catch( (error:any) => { return Observable.throw(error); }); 
    }

}