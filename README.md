#  Minimal API com Cache Distribuído (.NET)

Este projeto consiste em uma **Minimal API** desenvolvida em **.NET**, simulando um sistema de gerenciamento de usuários (**CRUD**).  
A aplicação utiliza o padrão de **Cache Distribuído** para armazenamento temporário de dados, demonstrando conceitos de **serialização JSON** e **organização modular de rotas**.

---

##  Funcionalidades

- **Criar (Create)**  
  Cadastro de usuários com persistência de estado via serialização JSON no cache.

- **Exibir (Read)**  
  Recuperação de dados utilizando o padrão de busca por chave única (**ID**).

- **Atualizar (Update)**  
  Validação da existência do registro antes da atualização do estado no cache.

- **Deletar (Delete)**  
  Limpeza do estado no cache em segundo plano.

---

##  Estrutura do Projeto

O código está organizado de forma a separar responsabilidades conforme o padrão MVC adaptado:

### 1. Model (`Usuario.cs`)
Define a estrutura do recurso utilizando o tipo `record`, garantindo imutabilidade e uma sintaxe simplificada.

**Campos:**
- `Id`
- `Nome`
- `Email`
- `Senha`

---

### 2. View (`Endpoints.cs`)
Contém o mapeamento modular das rotas utilizando `IEndpointRouteBuilder`.

- **Agrupamento:**  
  As rotas são agrupadas sob o prefixo `/api/usuario` para garantir consistência.

- **Tecnologia:**  
  Uso de `IDistributedCache` para manipulação assíncrona dos dados.

---

### 3. Controller (`Program.cs`)
Ponto de entrada da aplicação onde os serviços são configurados e inicializados:

- Ativação do `AddDistributedMemoryCache`
- Mapeamento das rotas customizadas através de `MapEndpointsRoutes`
- Configuração de redirecionamento HTTPS

---

##  Tecnologias Utilizadas

- .NET (C#)
- ASP.NET Core Minimal APIs
- `Microsoft.Extensions.Caching.Distributed` (`IDistributedCache`)
- `System.Text.Json` (Serialização)

---

##  Endpoints da API

URL base para consumo da API:  http://localhost:5000/api/usuario

| Método | Rota                             | Descrição                                   |
|------:|----------------------------------|---------------------------------------------|
| POST  | `/api/usuario`                   | Cria um novo usuário e armazena no cache    |
| GET   | `/api/usuario/exibir/{id}`       | Busca um usuário por ID                     |
| PUT   | `/api/usuario/atualizar/{id}`    | Atualiza um usuário existente               |
| DELETE| `/api/usuario/delete/{id}`       | Remove um usuário do cache                  |

---

##  Consumo da API

1. Certifique-se de ter o **SDK do .NET** instalado.
2. No diretório raiz do projeto, execute: dotnet run
3. Porta configurada no projeto: 5000
4. Para as operações de **Criação (POST)** e **Atualização (PUT)**, é obrigatório o envio de um **corpo da requisição no formato JSON**, contendo as informações do usuário.


A API foi projetada para ser consumida principalmente por **ferramentas de teste e clientes HTTP**, tais como:

- **Postman** (principal plataforma utilizada)
- **Insomnia**
- Outras ferramentas equivalentes compatíveis com requisições **REST**


##  Corpo da requisição JSON

```json
{
  "id": 1,
  "nome": "Douglas Barichello",
  "email": "exemplo@email.com",
  "senha": "123456"
}




