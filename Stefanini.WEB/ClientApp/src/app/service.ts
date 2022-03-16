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
  cidadeurl = "api/cidade"

  constructor(private http: HttpClient) { }

  //Pessoa
  listarpessoas() {
    return this.http.get<any>(this.pessoaurl);
  }

  cadastrarpessoa(pessoa: any) {
    return this.http.post(this.pessoaurl, pessoa, this.httpOptions);
  }

  deletarpessoa(id: number) {
    return this.http.delete(`${this.pessoaurl}/${id}`);
  }

  buscarpessoa(id: number) {
    return this.http.get<any>(`${this.pessoaurl}/${id}`);
  }

  atualizarpessoa(id: number, pessoa: any) {
    return this.http.put(`${this.pessoaurl}/${id}`, pessoa);
  }

  //Cidade
  listarcidades() {
    return this.http.get<any>(this.cidadeurl);
  }

  cadastrarcidade(pessoa: any) {
    return this.http.post(this.cidadeurl, pessoa, this.httpOptions);
  }

  deletarcidade(id: number) {
    return this.http.delete(`${this.cidadeurl}/${id}`);
  }

  buscarcidade(id: number) {
    return this.http.get<any>(`${this.cidadeurl}/${id}`);
  }

  atualizarcidade(id: number, cidade: any) {
    return this.http.put(`${this.cidadeurl}/${id}`, cidade);
  }

  carregarcidades(id: string) {
    return this.http.get<any>(`${this.cidadeurl}/carregarcidades/${id}`);
  }

}
