import { Component } from '@angular/core';
import { FormGroup, FormControl, AbstractControl, FormBuilder, Validators } from '@angular/forms'

import { AuthService } from '../services/auth.service';
     
@Component({
    selector: 'auth',
    templateUrl: 'views/auth.html',
    styleUrls: ['styles/auth.css']
})
export class AuthComponent { 
    form: FormGroup;
    login: AbstractControl;
    password: AbstractControl;
    loading: boolean;
    loginResponse: number;
    registerResponse: number;

    constructor(private auth: AuthService, private formBuilder: FormBuilder) {
        this.loading = false;
        this.loginResponse = 0;
        this.registerResponse = 0; // 0 means undefinded
        this.form = formBuilder.group({
            'login' : ['', this.loginValidator],
            'password' : ['', this.passwordValidator]
        });
        this.login = this.form.controls['login'];
        this.password = this.form.controls['password'];
    }

    loginValidator(control: FormControl) : { [s:string] : boolean } {
        if ( control.value.length < 3 )
            return { invalidLength : true };
    }

    passwordValidator(control: FormControl) : { [s:string] : boolean } {
        if ( control.value.length < 6 )
            return { invalidLength : true };
    }

    logIn() {
        this.loginResponse = 0;
        this.registerResponse = 0;
        this.loading = true;
        let login = this.login.value;
        let password = this.password.value;
        this.auth.login(login, password)
                 .then( (data) => {
                     this.loginResponse = 1; // ok
                     this.loading = false;
                 } )
                 .catch( (error) => {
                     this.loginResponse = 2; // error
                     this.loading = false;
                 });
    }

    register() {
        this.loginResponse = 0;
        this.registerResponse = 0;
        this.loading = true;
        let login = this.login.value;
        let password = this.password.value;
        this.auth.register(login, password)
                 .then( (data) => {
                     this.registerResponse = 1;
                     this.loading = false;
                 } )
                 .catch( (error) => {
                     this.registerResponse = 2;
                     this.loading = false;
                 });
    }
}