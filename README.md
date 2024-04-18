# Car Dealer Shop - (API) - Sistema de Gerenciamento de Carros

Este é um sistema de gerenciamento de carros desenvolvido para uma loja de carros fictícia controlar o valor dos carros novos que entram no estoque. A aplicação é dividida em duas partes: uma API RESTful desenvolvida em .NET Core para o backend e uma interface de usuário (UI) desenvolvida em React para o frontend.

## Backend (.NET Core)

A parte do backend da aplicação foi desenvolvida em .NET Core, uma estrutura robusta e eficiente para o desenvolvimento de aplicativos web e APIs RESTful. A API oferece endpoints para manipular carros, incluindo a adição, remoção, atualização e consulta de carros no estoque. Além disso, a API é responsável pela interação com o banco de dados MongoDB, onde os dados dos carros são armazenados.

## Frontend (React) (Procure o repo no meu perfil)

O frontend da aplicação foi desenvolvido em React, uma biblioteca JavaScript de código aberto amplamente utilizada para construir interfaces de usuário. A interface de usuário oferece uma experiência intuitiva para os usuários gerenciarem os carros no estoque. Os principais recursos incluem a visualização de carros disponíveis, adição de novos carros, edição de informações e remoção de carros do estoque.

## Recursos Principais

- Adição de novos carros ao estoque.
- Visualização detalhada de carros no estoque.
- Edição de informações de carros existentes.
- Remoção de carros do estoque.
- Interface de usuário responsiva e amigável.

## Pré-requisitos

- Node.js e npm instalados para o ambiente de desenvolvimento do frontend.
- .NET Core SDK instalado para o ambiente de desenvolvimento do backend.
- MongoDB instalado e configurado para o armazenamento de dados.

## Instruções de Instalação

1. Clone este repositório em sua máquina local.
2. Navegue até a pasta `frontend` e execute `npm install` para instalar as dependências do frontend.
3. Navegue até a pasta `backend` e execute `dotnet build` para compilar o projeto do backend.
4. Certifique-se de que o MongoDB esteja em execução em sua máquina local.
5. Navegue até a pasta `backend` e execute `dotnet run` para iniciar o servidor do backend.
6. Navegue até a pasta `frontend` e execute `npm start` para iniciar o servidor de desenvolvimento do frontend.
7. Abra seu navegador e acesse `http://localhost:3000` para visualizar a aplicação em execução.

## Contribuição

- Se você encontrar algum problema ou tiver alguma sugestão de melhoria, sinta-se à vontade para abrir uma issue ou enviar um pull request para este repositório.

## Licença

Este projeto está licenciado sob a [Licença MIT](https://opensource.org/licenses/MIT).
