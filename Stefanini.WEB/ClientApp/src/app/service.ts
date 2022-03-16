import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AppService {

  httpOptions = {
    headers: new HttpHeaders({
      'Access-Control-Allow-Origin': '*',
      'Authorization': 'authkey'
    })
  };

  pessoaurl = 'api/pessoa';

  constructor(private http: HttpClient) { }

  listarpessoas() {
    return this.http.get<any>(this.pessoaurl);
  }

  cadastrarpessoa(pessoa: any) {
    return this.http.post(this.pessoaurl, pessoa, this.httpOptions);
  }

}
