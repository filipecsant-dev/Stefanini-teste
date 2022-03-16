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
  showList = true;
  error: any[] = [];
  cidades: any[] = [];
  estados: any[] = [];
  estado = null;
  pessoa: Pessoa = {
    nome: "",
    idade: 0,
    cpf: "",
    cidade: {
      nome: "",
      uf: ""
    }
  };


 

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

    this.service.listarcidades()
      .subscribe((res: any) => {
        this.estados = res.dados;
      });
  }

  cadastrarpessoa() {
    if (this.pessoa.nome.length > 300) {
      this.error = [{ erro: "Nome muito longo!" }];
      return;
    }
    if (this.pessoa.nome.length < 5) {
      this.error = [{ erro: "Nome muito curto." }];
      return;
    }
    if (this.pessoa.cpf.length != 11) {
      this.error = [{ erro: "CPF inválido!" }];
      return;
    }
    if (this.pessoa.idade < 1) {
      this.error = [{ erro: "Informe um idade válida!" }];
      return;
    }
    if (this.pessoa.cidade.uf == "") {
      this.error = [{ erro: "Informe o Estado" }];
      return;
    }
    if (this.pessoa.cidade.nome == "") {
      this.error = [{ erro: "Informe a Cidade" }];
      return;
    }

    this.error = [];

    this.service.cadastrarpessoa(this.pessoa).subscribe((res) => {
      location.reload();
    });
  }

  deletar(id: number): void {
    this.service.deletarpessoa(id).subscribe(() => {
      location.reload();
      
    }, (err) => {
      console.log(err.error);
      this.error = err.message;
      // In this block you get your error message
      // as "Failed to create new user"
    });
  }

  exibeCidades() {
    this.service.carregarcidades(this.pessoa.cidade.uf)
      .subscribe((res: any) => {
        this.cidades = res;
      });
   
  }


  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }

}

interface Pessoa {
  nome: string;
  idade: number;
  cpf: string;
  cidade: Cidade;
}

interface Cidade {
  nome: string;
  uf: string;
}
