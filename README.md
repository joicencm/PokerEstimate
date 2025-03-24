# Sistema de Estimativas de Tarefas (Poker Estimation)

Este projeto implementa uma sala de estimativas utilizando o método de Poker Planning para equipes de desenvolvimento e QA. A aplicação permite que os membros da equipe criem ou entrem em uma sala para estimar tarefas utilizando pontos, facilitando a colaboração e a tomada de decisões rápidas.

## Funcionalidades

- **Criação e Entrada em Sala**: Permite aos usuários criar uma nova sala ou entrar em uma sala existente.
- **Estimativas**: Os usuários podem selecionar uma estimativa para a tarefa, utilizando valores predefinidos (como 0, 1, 2, 3, entre outros), ou símbolos como `coffee` e `?`.
- **Exibição de Resultados**: O criador da sala pode exibir os resultados de todas as estimativas feitas pelos membros da equipe.
- **Limpeza de Votos**: O criador pode limpar os votos realizados por todos os membros, reiniciando a estimativa da tarefa.
- **Responsivo**: A aplicação é otimizada para ser utilizada em dispositivos móveis e desktops.

## Estrutura do Projeto

O projeto segue uma estrutura MVC (Model-View-Controller) para organização do código:

### Controllers

Os controladores (Controllers) são responsáveis pela lógica de negócio da aplicação e manipulação dos dados entre o modelo e as views. Eles gerenciam ações como a criação de salas, entrada de usuários, registro de estimativas e exibição dos resultados.

- **Exemplo**: Controladores para `CriarSala`, `EntrarSala`, `RegistrarEstimativa`, entre outros.

### Models

Os modelos representam as entidades principais da aplicação, como `Sala` e `Usuario`. Eles contêm as informações necessárias para manipulação dos dados e são utilizados pelos controladores.

- **Sala**: Representa a sala de estimativas com informações como o ID da sala, criador, estimativas dos usuários, etc.
- **Usuario**: Representa os usuários que participam da sala, incluindo o nome, tipo (DEV, QA) e a estimativa feita.

### Views

As views são responsáveis pela exibição das informações para os usuários. Elas são compostas por HTML e código Razor (ASP.NET), permitindo a renderização dinâmica dos dados.

- **Página Inicial**: Exibe as opções para criar uma nova sala ou entrar em uma existente.
- **Painel de Estimativa**: Exibe a sala de estimativas, onde os membros podem selecionar sua estimativa e visualizar os resultados.
- **Resultado Final**: O criador da sala pode visualizar os resultados após todos os membros terem estimado a tarefa.

## Tecnologias Utilizadas

- **ASP.NET Core**: Framework principal para desenvolvimento da aplicação web.
- **Razor Views**: Utilizado para renderizar páginas dinâmicas.
- **SignalR**: Tecnologia para comunicação em tempo real entre os usuários.
- **Bootstrap**: Framework CSS para criação de layouts responsivos.
- **JavaScript**: Utilizado para manipulação do DOM e interatividade.

## Instruções de Uso

1. **Clonar o repositório**:

   ```bash
   git clone https://github.com/usuario/repositorio.git
   ```
