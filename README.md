# api-challenger

Esta é a minha primeira API desenvolvida tendo como referência um Challenger para desenvolvedores Back-End da empresa Goomer, feita em .NET 6 utilizando Entity Framework Core para acessar os dados num banco de dados.

A API é capaz de criar um novo restauraunte com: Nome, Foto do Restaurante e Endereço. É possível criar e relacionar produtos a este restaurante criados por meio de um controller específico, e a estes produtos é possível relacionar uma ou mais promoções.

Ao controller do Restaurante, eu implementei um CRUD básico para: listar todos os restaurantes; buscar um restaurante específico utilizando como parâmetro o seu ID; cadastrar novos restaurantes; excluir restaurantes; listar todos os produtos de um restaurante.
Ao restaurante, é possível também relacionar todo um quadro de horários de Domingo a Sexta-Feira.
