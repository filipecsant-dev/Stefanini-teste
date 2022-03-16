import { Component, OnInit, OnDestroy } from '@angular/core';
import { AppService } from '../service';
import { FormGroup } from '@angular/forms';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit, OnDestroy {
  public dtOptions: DataTables.Settings = {};
  public dtTrigger: Subject<any> = new Subject<any>();
  public data: any;

  constructor(private service: AppService) { }

  ngOnInit(): void {

    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10,
      language: { url: 'https://cdn.datatables.net/plug-ins/1.11.5/i18n/pt-BR.json' },
      processing: true
    };

    this.service.listarpessoas()
      .subscribe((res: any) => {
        this.data = res.dados;
        this.dtTrigger.next();
      });
  }

  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }

}
