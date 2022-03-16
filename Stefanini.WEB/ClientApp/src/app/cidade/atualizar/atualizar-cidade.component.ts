import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppService } from '../../service';

@Component({
  selector: 'app-atualizar-cidade',
  templateUrl: './atualizar-cidade.component.html'
})
export class AtualizarCidadeComponent implements OnInit {

  constructor(private service: AppService, private activatedRouter: ActivatedRoute) { }

  error: any[] = [];
  cidade: Cidade = {
    id: 0,
    uf: "",
    nome: ""
  };

  meuid = Number(this.activatedRouter.snapshot.paramMap.get('id'));

  ngOnInit(){
    const id = Number(this.activatedRouter.snapshot.paramMap.get('id'));

    this.service.buscarcidade(id)
      .subscribe(
        (res: any) => { this.cidade = res.dados; },

        (erro) => {
          this.error = [{ erro: erro.error }];
        }
      );
  }

  atualizarcidade() {
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

    this.service.atualizarcidade(this.meuid, this.cidade)
      .subscribe(
        () => { },

        (erro) => {
          this.error = [{ erro: erro.error }];
        },

        () => { location.reload(); }
      );
  }
}

interface Cidade {
  id: number;
  nome: string;
  uf: string;
}

