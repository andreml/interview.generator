
<a href='https://postimages.org/' target='_blank'><img src='https://i.postimg.cc/gJfV2KJk/logo.jpg' border='0' alt='logo'/></a>


# Skill Check

Sistema de focado em empresas de recrutamento que desejam aplicar testes em candidatos durante as entrevistas para uma vaga de emprego.

O Sistema permite cadastrar perguntas categorizadas em áreas de conhecimento, criar questionários com as perguntas cadastradas, aplicar avaliações com os questionários cadastrados para os candidatos e visualizar os resultados e estatísticas dos questionários aplicados.

## Autores

- [@guilhermeTaira](https://github.com/guilhermeTaira)
- [@lirajon1988](https://github.com/lirajon1988)
- [@andreml](https://github.com/andreml)
- [@Daniellyaraujo](https://github.com/Daniellyaraujo)

## DDD

<a href='https://miro.com/app/board/uXjVNdPoMRc=/?share_link_id=903496239789' target='_blank'>Link do Miro</a>

## Stack utilizada

**Back-end:** .Net 7, MSSQL, EF Core, FluentValidation e XUnit

## Entidades e requisitos
Identificamos as seguintes entidades de domínio em nosso projeto:

### Usuário
**Definição:** usuários habilitados para uso do sistema

#### Perfis para cadastro de usuário
O usuário do sistema pode ter apenas um dos dois perfis abaixo:
-	Candidato: perfil habilitado para responder as avaliações enviadas pelo avaliador
-	Avaliador: perfil habilitado a fazer os cadastros de perguntas, alternativas, questionários e avaliações para envio
  
#### Principais validações para cadastro de usuário
-	Um usuário pode ter apenas um perfil cadastrado

### Pergunta

**Definição:** uma pergunta cadastrada com um conjunto de alternativas

#### Critérios de aceite para cadastro de pergunta

- Uma **pergunta** deve ter apenas uma **alternativa** correta
- Uma **pergunta** deve ter entre 3 e 5 **alternativas**
- Uma **pergunta** deve ter apenas uma **área de conhecimento**
- Não é possível excluir uma **pergunta** se ela já foi utilizada em um **questionário**

### Questionário

**Definição:** Conjunto de perguntas selecionadas para criação de um questionário

#### Critérios de aceite para cadastro de questionário:

- Um **questionário** deve ter no mínimo 3 **perguntas** e no máximo 50 **perguntas**
- Um **questionário** não pode ser alterado se já estiver atrelado a uma **avaliação**
- Um **questionário** não pode ser apagado se já estiver atrelado a uma **avaliação**

### Avaliação

**Definição:** seleção de um questionário que será enviado a um candidato, que deverá responder e devolver ao avaliador

#### Critérios de aceite para cadastro de avaliação

- Para cadastrar uma **avaliação** nova, deve ser selecionado um **questionário** e um **candidato**
- Uma **avaliação** pode ser enviada a um **candidato** apenas uma vez

#### Critérios de aceite para um candidato responder a uma avaliação

- Um **candidato** pode responder uma avaliação somente uma vez
- Um **candidato** pode visualizar apenas **avaliações** cadastradas para ele próprio

### Alternativa

**Definição:** Conjunto de possíveis respostas para uma pergunta

#### Critérios de aceite para cadastro de alternativa

- Uma **alternativa** não pode ser repetida em uma **pergunta**

### Área de conhecimento

**Definição:** Classificação para uma pergunta para facilitar sua localização

#### Critérios de aceite para cadastro de área de conhecimento

- Uma **área de conhecimento** pode ser incluída junto com uma **pergunta**

### Resposta da avaliação

**Definição:** a alternativa selecionada para uma pergunta que será respondida pelo candidato

#### Critérios de aceite para resposta de avaliação

- Somente podem ser informada uma **resposta da avaliação** para uma **pergunta** de acordo com as **alternativas** cadastradas na **pergunta**

###

## Funcionalidades

#### Área de conhecimento (Categorias de perguntas)
 - Adicionar áreas de conhecimento (Perfil Avaliador)
 - Obter áreas de conhecimento cadastradas (Perfil Avaliador)
 - Alterar área de conhecimento (Perfil Avaliador)

#### Avaliação	(Aplicação de um Questionário)
- Adicionar avaliação de um candidato (Perfil Candidato)
- Adicionar observações em uma avaliação (Perfil Avaliador)
- Obter avaliações cadastradas (Perfil Avaliador)

#### Login
 - Gerar token de acesso (Perfil Avaliador | Candidato)

#### Pergunta	
- Adicionar pergunta relacionada a uma área de conhecimento (Perfil Avaliador)
- Alterar uma pergunta cadastrada (Perfil Avaliador)
- Obter perguntas cadastradas (Perfil Avaliador)
- Excluir uma pergunta cadastrada (Perfil Avaliador)

#### Questionário (Conjunto de perguntas)
- Adicionar um novo Questionário (Perfil Avaliador)
- Alterar um questionário existente (Perfil Avaliador)
- Excluir um questionário existente (Perfil Avaliador)
- Obter questionários cadastrados (Perfil Avaliador)
- Obter um questionário espeífico para preenchimento (Perfil Candidato)
- Obter estatísticas de um questionário (Perfil Avaliador)

#### Usuário	
- Adicionar um usuário no sistema (Perfil Avaliador | Candidato)
- Alterar um usuário cadastrado (Perfil Avaliador)
- Obter usuário (Perfil Avaliador | Candidato)
## BUILD

Para fazer o build desse projeto rode

```bash
  dotnet restore
```

```bash
  dotnet build
```

## Executando Localmente - Banco de dados

Utilizamos o arquivo 'docker-compose.yml' da raiz do projeto para criar o banco de dados localmente utilizando docker:

- Baixar o docker no Windows em https://docker.com/products/docker-desktop/ e instale em sua máquina
- Abra o PowerShell do Windows como Administrador
- Acessar pasta da Solução interview.generator
- Digite o comando abaixo 

```bash
docker-compose -f .\docker-compose.yml up
```
<a href='https://postimages.org/' target='_blank'><img src='https://i.postimg.cc/MZvtJFyB/powershell.png' border='0' alt='powershell'/></a><br />

## Executando Localmente - API

Clone o projeto

```bash
  git clone https://github.com/andreml/interview.generator
```

Abra o Visual Studio e no Package Manager Console entre no diretório do projeto

```bash
   cd .\src\InterviewGenerator.Api\
```

Instale as dependências

```bash
  dotnet restore
```

```bash
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 7.0.13
```

Altere o Default project no Package Manager Console para src\InterviewGenerator.Infra conforme abaixo
![image](https://github.com/andreml/interview.generator/assets/18474627/1a235e4d-2ffb-445b-b46e-28173933d6dd)

Execute o comando abaixo

```bash
  Update-Database
```

Inicie o servidor

```bash
  dotnet run --project InterviewGenerator.Api.csproj --property:Configuration=Release --port 5133
```

Abra o navegador

```bash
   E digite o endereço http://localhost:5133/swagger/index.html
```

## Executando os testes

Para executar os testes, rode o seguinte comando

```bash
  cd .\test\InterviewGenerator.IntegrationTests\
```

```bash
  dotnet test InterviewGenerator.IntegrationTests.csproj --logger "html;logfilename=testResults.html"
```

O teste result ficará salvo na pasta abaixo

```bash
  cd .\test\InterviewGenerator.IntegrationTests\TestResults\
```
 
 ## Usando SQL Server para conectar com o Banco

Caso deseje se conectar ao banco local para visualizar os dados, o endereço de conexão é o mesmo que está configurado no appsettings do projeto e no arquivo 'docker-compose.yml':

```bash
Nome do servidor: 127.0.0.1
Login: sa
Senha: interview@2023
```
 <a href='https://postimages.org/' target='_blank'><img src='https://i.postimg.cc/7YpqndNn/sql-server-login.png' border='0' alt='sql-server-login'/></a>

 - Consultando a tabela usuários após a inclusão
```bash
USE InterviewGenerator
GO

SELECT * FROM USUARIO
```

<a href='https://postimages.org/' target='_blank'><img src='https://i.postimg.cc/9fyPQ6L8/consulta-sql.png' border='0' alt='consulta-sql'/></a>
