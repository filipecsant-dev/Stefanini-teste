import { Component, OnInit, OnDestroy } from '@angular/core';
import { AppService } from '../service';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit, OnDestroy {
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();
  data: any[] = [];
  pessoa: any = {};

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

  cadastrarpessoa() {
    this.service.cadastrarpessoa(this.pessoa).subscribe((res: any) => {
      location.reload();

      this.pessoa = {};
    });
  }

  deletar(id: number): void {
    this.service.deletarpessoa(id).subscribe(() => {
      location.reload();
    });
  }

  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }

}
