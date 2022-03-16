import { Component, OnInit, OnDestroy } from '@angular/core';
import { AppService } from '../service';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-cidade',
  templateUrl: './cidade.component.html',
})
export class CidadeComponent implements OnInit, OnDestroy {
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();
  data: any[] = [];
  error2: any[] = [];
  error: any[] = [];
  showList = true;
  cidade: Cidade = {
    id: 0,
    nome: "",
    uf: ""
  };

  constructor(private service: AppService) { }

  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10,
      language: { url: 'https://cdn.datatables.net/plug-ins/1.11.5/i18n/pt-BR.json' },
      processing: true
    };

    this.service.listarcidades()
      .subscribe(
        (res: any) => {
          this.data = res.dados;
          this.dtTrigger.next();
        },

        (erro) => {
          this.error2 = [{ erro: erro.error }];
        }
      );
  }

  cadastrarcidade() {
    if (this.cidade.uf.length > 2 || this.cidade.uf.length < 1) {
      this.error = [{ erro: "Informe o UF do estado válido." }];
      return;
    }

    if (this.cidade.nome.length > 200) {
      this.error = [{ erro: "Nome muito longo!" }];
      return;
    }
    if (this.cidade.nome.length < 3) {
      this.error = [{ erro: "Nome muito curto!" }];
      return;
    }


    this.service.cadastrarcidade(this.cidade)
      .subscribe(
        () => { },

        (erro) => {
          this.error = [{ erro: erro.error }];
        },

        () => { location.reload(); }
      );
  }

  deletar(id: number): void {
    this.service.deletarcidade(id)
      .subscribe(
        () => { },

        (erro) => {
          this.error = [{ erro: erro.error }];
        },

        () => { location.reload(); }
      );
  }

  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }

}

interface Cidade {
  id: number;
  nome: string;
  uf: string;
}
