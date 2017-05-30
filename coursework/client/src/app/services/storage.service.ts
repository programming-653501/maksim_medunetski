import { Injectable } from '@angular/core';

@Injectable()
export class StorageService {

    constructor() {
        
    }

    set(key: string, value: string) {
        localStorage.setItem(key, value);
    }

    get(key: string) : string|null {
        return localStorage.getItem(key);
    }

    delete(key: string) {
        localStorage.removeItem(key);
    }
}