# DexQuiz

# Adding migrations

You can add migrations using this command
```
dotnet ef migrations add [NAME] --context DexQuizContext --startup-project ..\DexQuiz.Server\
```

# Regras do Quiz
```
1 - Existem diversas trilhas no DexQuiz, o usuário pode responder uma única vez cada uma delas
2 - Cada trilha possui diversas perguntas no banco de dados, porém um usuário irá responder apenas 12 perguntas por trilha (4 de cada nível)
3 - Existem três níveis de perguntas: Fácil, Médio e Difícil
4 - Existem dois tipos de Ranking, o geral e o por trilha, cada um deles possui seus próprios prêmios
5 - Por questão de dados sensíveis o usuário não conseguirá ver o nome dos participantes no ranking, apenas a pontuação deles
6 - O usuário administrador (Dextra) consegue ver todas as informações do ranking para saber quem são os ganhadores
```
