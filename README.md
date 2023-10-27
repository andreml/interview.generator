
![Logo](https://dev-to-uploads.s3.amazonaws.com/uploads/articles/th5xamgrr6se0x5ro4g6.png)


# Job Wave

Sistema de focado em empresas de recrutamento que desejam aplicar testes em candidatos durante as entrevistas para uma vaga de emprego.

O Sistema permite cadastrar por áreas de conhecimento, realizar testes com perguntas aletórias para candidatos.


## Autores

- [@guilhermeTaira](https://github.com/guilhermeTaira)
- [@lirajon1988](https://github.com/lirajon1988)
- [@andreml](https://github.com/andreml)


## Stack utilizada

**Back-end:** .Net 7 


## Funcionalidades

#### Área de conhecimento
 - Adicionar área de conhecimento (Perfil Avaliador)
 - Obter áreas de conhecimento cadastradas (Perfil Avaliador)
 - Alterar área de conhecimento (Perfil Avaliador)

#### Avaliação	
- Adicionar avaliação de um(a) candidato (Perfil Candidato)
- Adicionar observações de uma avaliação (Perfil Avaliador)
- Obter avaliações cadastradas (Perfil Avaliador)

#### Login
 - Gerar token de acesso (Perfil Avaliador | Candidato)

#### Pergunta	
- Adicionar pergunta relacionada a uma área de conhecimento (Perfil Avaliador)
- Alterar uma pergunta cadastrada (Perfil Avaliador)
- Obter perguntas cadastradas (Perfil Avaliador)
- Excluir uma pergunta cadastrada (Perfil Avaliador)

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

## Rodando localmente

Clone o projeto

```bash
  git clone https://github.com/andreml/interview.generator
```

Entre no diretório do projeto

```bash
   cd .\src\interview.generator.api\
```

Instale as dependências

```bash
  dotnet restore
```

Inicie o servidor

```bash
  dotnet run --project interview.generator.api.csproj --property:Configuration=Release
```

Abra o navegador

```bash
   E digite o endereço http://localhost:5133/swagger/index.html

   Usuário de teste:
   Avaliador:
     login: avaliador
     senha: avaliador@2023
   
   Candidato:
     login: candidato
     senha: candidato@2023
```

## Rodando os testes

Para rodar os testes, rode o seguinte comando

```bash
  cd .\test\Interview.Generator.IntegrationTests\
```

```bash
  dotnet test Interview.Generator.IntegrationTests.csproj --logger "html;logfilename=testResults.html"
```

O teste result ficará salvo na pasta abaixo

```bash
  cd .\test\Interview.Generator.IntegrationTests\TestResults\
```
