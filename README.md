<img src="https://www.comotrabalhar.org/wp-content/uploads/2016/06/trabalhar-na-radix.jpg" height="50px" alt="Desafio Radix"> 


# Sensor API
O objetivo desta Api implementada em .NET Core é receber milhares de eventos de sensores espalhados pelo Brasil, nas regiões norte, nordeste, sudeste e sul.

## Como usar:
- Você pode acessar diretamente a API publicada na Azure em https://sensor.azurewebsites.net/swagger/index.html.

## Para acessar local:
- Você precisará do mais recente Visual Studio 2017 e do mais recente .NET Core SDK.
- ** Verifique se você instalou a mesma versão de tempo de execução (SDK) descrita em global.json **
- O SDK e as ferramentas mais recentes podem ser baixados em https://dot.net/core.

## Tecnologias implementadas:

- ASP.NET Core 2.2 (com o .NET Core 2.2)
 - ASP.NET WebApi Core
- .NET núcleo nativo DI
- FluentValidator
- MediatR
- MongoDB
- Application Insights
- Interface do Swagger

## Ideias

- Foi implementado na solução o serviço de ESB da Microsoft porém trecho que envia mensagem para o serviço na Azure está comentada, como foi solicitado no desafio que o mesmo deveria está publicado. 
O projeto resposável por realizar a baixa das mensagens enfileiradas seria uma Function que seria disparada através trigger que sob demanda iria processando em lotes as mensagens recebidas. Esta ideia seria para ter como solução em caso de qualquer falha na API de processamento eu não perderia os eventos recebidos, pois ficariam todos enfileirados e assim que o serviço de processamento estivesse disponivel as mesagens seriam registradas.