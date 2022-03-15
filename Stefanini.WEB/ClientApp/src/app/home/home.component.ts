import { Component, OnInit, OnDestroy } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit, OnDestroy {
  dtOptions: DataTables.Settings = {};
  dtTrigger = new Subject();
  public data: any;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 5
    };

    this.http.get('https://localhost:7030/api/pessoa')
      .subscribe((res: any) => {
        this.data = res.dados;
        this.dtTrigger.next();
        console.log(res);
      });
  }

  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }

}

interface Pessoa {
  nome: string;
  cpf: string;
  idade: number;

};
