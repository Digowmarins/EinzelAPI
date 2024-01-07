# API Einzel

**Nota:** Este projeto deve ser usado em conjunto com a [LojaEinzel](https://github.com/Digowmarins/Einzel).

Bem-vindo à API Einzel, uma poderosa solução desenvolvida em .NET para alimentar o site de compras de roupas Einzel.

## Visão Geral

A API Einzel foi projetada para fornecer funcionalidades de backend essenciais ao site de compras Einzel, abrangendo desde o gerenciamento de produtos até a autenticação segura de usuários. Com uma arquitetura robusta, esta API é a espinha dorsal que impulsiona uma experiência de compra eficiente e segura.

## Funcionalidades Principais

### 1. Gerenciamento de Produtos

- **Adição, Atualização e Exclusão de Produtos:**
  - Adicione novos produtos, atualize informações existentes e remova produtos conforme necessário.
  
- **Detalhes do Produto:**
  - Obtenha informações detalhadas sobre os produtos, proporcionando uma experiência de compra informada para os usuários.

- **Imagens de Produtos:**
  - Enriqueça a experiência visual do usuário permitindo a adição de imagens aos produtos.

### 2. Gerenciamento de Pedidos

- **Recebimento e Processamento de Pedidos:**
  - Receba e processe pedidos de maneira eficiente, mantendo a integridade das transações.

- **Acompanhamento de Status:**
  - Acompanhe o status dos pedidos para fornecer atualizações em tempo real aos usuários.

### 3. Gerenciamento de Usuários

- **Registro Seguro:**
  - Registre novos usuários de forma segura, utilizando recursos avançados do Entity Framework e Identity.

- **Autenticação Protegida:**
  - Implemente autenticação segura para garantir que apenas usuários autorizados tenham acesso às funcionalidades essenciais.

### 4. Notificações

- **Comunicação Eficiente:**
  - Envie e-mails aos clientes informando sobre o status dos pedidos, criação de contas e alterações de senhas, mantendo uma comunicação eficaz.

## Tecnologias Utilizadas

- **Entity Framework:**
  - Utilizado para mapeamento objeto-relacional (ORM) e gerenciamento eficaz do banco de dados.

- **Identity:**
  - Oferece recursos avançados para autenticação e autorização de usuários, garantindo a segurança do sistema.

- **Segurança de Senhas:**
  - As senhas dos usuários são armazenadas com hash e salt para garantir a máxima segurança.
