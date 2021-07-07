# Decisões técnicas e arquiteturais

- Separado leitor de input em uma extensão, para simplificar o código da Main
- Objetos usados para conversão separados em pasta própria

# Justificativa de uso de frameworks e bibliotecas

## Newtonsoft Json

Usado comumente para converter jsons para objetos e objetos para json
em .NET. Recomendado inclusive pela Microsoft.

# Como compilar e executar

## Fazer o build do docker no root do projeto

```
docker build . -t authorizer
```

## Rodar a máquina colocando a pasta da solução como volume

```
REM windows
docker run -it -v %cd%\authorizer:/var/auth authorizer
```

```
# linux
docker run -it -v $(pwd)/authorizer:/var/auth authorizer
```

## Rodar o projeto

```
dotnet build
./bin/Debug/net5.0/Authorizer
```

# Notas adicionais para avaliação
