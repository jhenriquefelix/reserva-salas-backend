# 📅 Reserva de Salas

Projeto de **estudo** para gerenciamento de reservas de salas, desenvolvido em **.NET 8** com **Entity Framework Core** e **SQLite**.

Repositório: [https://github.com/jhenriquefelix/reserva-salas-backend](https://github.com/jhenriquefelix/reserva-salas-backend)

---

## 🛠 Tecnologias Utilizadas
- **.NET 8** (ASP.NET Core Web API)
- **Entity Framework Core 8**
- **SQLite** (banco de dados leve e simples)
- **FluentValidation** (validação de dados)
- **Swagger** (documentação e testes da API)

---

## 📂 Estrutura do Projeto
- **ReservaSalas.Api** → Camada de API (controllers e configuração)
- **ReservaSalas.Aplicacao** → Casos de uso, DTOs e serviços de aplicação
- **ReservaSalas.Dominio** → Entidades e regras de negócio
- **ReservaSalas.Infraestrutura** → Acesso a dados, repositórios e migrations

---

## 🚀 Como Executar Localmente

Clonar o repositório
git clone https://github.com/jhenriquefelix/reserva-salas-backend.git
cd reserva-salas

Restaurar pacotes
dotnet restore

Criar o banco de dados
dotnet ef database update --project ReservaSalas.Infraestrutura --startup-project ReservaSalas.Api

Rodar a aplicação
dotnet run --project ReservaSalas.Api

Acessar no navegador
https://localhost:7009/swagger

Dados iniciais
O projeto já cria automaticamente:
Locais: Matriz, Filial
Salas: 3 salas por local


