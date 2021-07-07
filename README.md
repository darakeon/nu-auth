# Decisões técnicas e arquiteturais

- Separado leitor de input em uma extensão, para simplificar o código do programa
- Objetos usados para conversão separados em pasta própria
- Impressão de resultado no programa em si, ao invés de dentro do processador
- Adicionado teste automatizado em projeto a parte, para separação de conceitos
- Testes de unidade não foram adicionados para serem mantidos os métodos privados

# Justificativa de uso de frameworks e bibliotecas

## Newtonsoft Json

Usado comumente para converter jsons para objetos e objetos para json
em .NET. Recomendado inclusive pela Microsoft.

# Compilar e executar

Pode ser feito com a imagem de dotnet e executando os comandos
manualmente ou pode ser usada a máquina já copiando o autorizador
para dentro dela. Ela roda os testes em seu build e é baseada na
imagem dotnet.

## Build manual do projeto

Rodar build na raiz do projeto:

```
docker build . -t local-dotnet -f local-dotnet.dockerfile
```

Para iniciar máquina em windows:

```
docker run -it -v %cd%\authorizer:/var/auth local-dotnet
```

Para iniciar máquina em linux:

```
docker run -it -v $(pwd)/authorizer:/var/auth local-dotnet
```

### Rodar testes dentro do docker

```
dotnet test
```

### Rodar o projeto dentro do docker

```
dotnet build
./Authorizer/bin/Debug/net5.0/Authorizer < <caminho-do-arquivo>
```

ex:
```
./Authorizer/bin/Debug/net5.0/Authorizer < Test/scenarios/transaction_multiple_violation_input
```

# Notas adicionais para avaliação

Casos de testes diferentes foram adicionados dentro da pasta scenarios.
Ao rodar os testes do projeto, aparece como um teste só, mas a mensagem
traz qual o teste deu erro. Se der erro em mais de um, ele junta todos
os erros para retornar depois.
