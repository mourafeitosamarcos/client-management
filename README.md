# Client Management API

## Arquitetura Clean

Este projeto segue a arquitetura **Clean Architecture**, que separa as responsabilidades em camadas bem definidas:

- **Domain**: Cont�m as entidades de neg�cio e interfaces (regras de neg�cio puras, sem depend�ncias externas).
- **Application**: Implementa casos de uso e l�gica de aplica��o, orquestrando as entidades do dom�nio.
- **Infrastructure**: Respons�vel por detalhes de implementa��o como acesso a dados (EF Core), servi�os externos, etc.
- **WebApi**: Camada de apresenta��o, exp�e endpoints HTTP e injeta depend�ncias das camadas internas.

Essa separa��o facilita testes, manuten��o e evolu��o do sistema, al�m de isolar depend�ncias externas.

---

## Banco de Dados e Migra��es

Nesse Projeto Optei por usar o **PostgreSQL** como banco de dados relacional, por ser um banco de baixo custo e Robusto oferece o que o projeto precisa em um MVP e 
tambem o que ele precisa no futuro, al�m disso pode ser utilizado tanto no onprimece quanto em todos os clouds com custo relativo

![Diagrama do Banco de Dados](banco.png)

O projeto utiliza **Entity Framework Core** para gerenciamento do banco de dados. As migra��es s�o criadas e aplicadas com os comandos:

- O projeto `Infrastructure` cont�m o contexto e as migra��es.
- O projeto `WebApi` � usado como startup para configura��o de depend�ncias.

> **Nota:** Futuramente, ser� adicionada uma imagem ilustrando o desenho das classes e a estrutura do banco de dados, facilitando a visualiza��o das entidades e seus relacionamentos.

### Exemplo de execu��o de migra��es passo a passo
### Exemplo de execu��o de migra��es passo a passo

1. **Criar uma nova migration**  
   No terminal, navegue at� a raiz da solu��o e execute

  `dotnet ef migrations add NomeDaMigration --project Infrastructure --startup-project WebApi`
   - `--project Infrastructure`: indica onde est� o DbContext e onde as migra��es ser�o salvas.
   - `--startup-project WebApi`: indica o projeto de inicializa��o para carregar as configura��es.`
1. **Criar uma nova migration**  
   No terminal, navegue at� a raiz da solu��o e execute:
 
  `dotnet ef database update --project Infrastructure --startup-project WebApi` 
   - `--project Infrastructure`: indica onde est� o DbContext e onde as migra��es ser�o salvas.
   - `--startup-project WebApi`: indica o projeto de inicializa��o para carregar as configura��es.

2. **Aplicar as migra��es ao banco de dados**3. **Verificar o status das migra��es**4. **Remover a �ltima migration (caso necess�rio)**---

## Seguran�a de Senhas

Para garantir o armazenamento seguro de senhas, **N�O** s�o utilizados algoritmos r�pidos como MD5, SHA1 ou SHA256 puro, pois s�o vulner�veis a ataques de for�a bruta.

Neste projeto, a prote��o das senhas � realizada utilizando o algoritmo de deriva��o de chave PBKDF2, implementado no .NET por meio da classe `Rfc2898DeriveBytes`. Esse m�todo aplica um salt exclusivo e m�ltiplas itera��es para derivar o hash da senha, tornando o processo de quebra significativamente mais dif�cil para atacantes.

Alternativamente, tamb�m seria poss�vel utilizar algoritmos modernos como Argon2 ou bcrypt, dispon�veis via bibliotecas externas.

O uso do PBKDF2 com salt e itera��es elevadas proporciona uma camada robusta de seguran�a, alinhada com as melhores pr�ticas de prote��o de credenciais.

## Autentica��o JWT

O projeto utiliza **JWT (JSON Web Token)** para autentica��o, com as seguintes caracter�sticas:

- **Tipo**: Sim�trico (mesma chave secreta para assinar e validar tokens)
- **Algoritmo de assinatura**: HMAC SHA-256 (`SecurityAlgorithms.HmacSha256`)
- **Fluxo**:
  - Recebe `userId`, `email` e `roles`
  - Cria claims padr�o (`sub`, `email`, `jti`, `iat`) e claims adicionais
  - Gera token JWT assinado com a chave secreta
  - Valida token conferindo assinatura, emissor (`issuer`), audi�ncia (`audience`) e expira��o (`exp`)
  - Permite extra��o de claims conforme necess�rio

**Importante:** N�o utiliza par de chaves p�blica/privada (n�o � assim�trico como RS256).

---

## Resumo

- Arquitetura Clean para separa��o de responsabilidades
- Migra��es e atualiza��o de banco via EF Core
- Senhas protegidas com hashing seguro e salt
- Autentica��o JWT sim�trica com HMAC SHA-256
