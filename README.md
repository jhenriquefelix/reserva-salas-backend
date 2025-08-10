Reserva de Salas
Projeto de prova técnica para gerenciamento de reservas de salas, desenvolvido em .NET 8, Entity Framework Core e SQLite.

🛠 Tecnologias
.NET 8 (ASP.NET Core Web API)
Entity Framework Core
SQLite (banco de dados)
FluentValidation
Swagger (documentação e testes da API)

📂 Estrutura
ReservaSalas.Api → Camada de API
ReservaSalas.Aplicacao → Casos de uso e serviços de aplicação
ReservaSalas.Dominio → Entidades e regras de negócio
ReservaSalas.Infraestrutura → Acesso a dados e repositórios

Como rodar o projeto
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


