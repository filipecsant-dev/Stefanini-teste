import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AppService {

  pessoaurl = 'https://localhost:7030/api/pessoa';

  constructor(private http: HttpClient) { }

  listarpessoas() {
    return this.http.get<any>(this.pessoaurl);
  }

}
