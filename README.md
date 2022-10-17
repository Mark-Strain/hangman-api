# Hangman API

### Setup

API is using enitiy framework and is setup for SQLServer. To get started in Visual Studio update the connection string in the appsettings.json then run update-database in package manager console.

### Create new Hangman Game

#### Request

```
POST /api/hangman
```

#### Response

```
{
    "gameId": game_id,
    "word": "hangman_word"
}
```

### Guess a letter

#### Request

```
POST /api/hangman/{id}/guess
```

###### Body

```
{
    "letter": "d"
}
```

#### Response

```
{
    "gameId": game_id,
    "correct": true|false,
    "word": "hangman_word"
}
```

### Get Game

#### Request

```
GET /api/hangman/{id}
```

#### Response

```
{
    "gameId": game_id,
    "word": "hangman_word"
}
```

### Get Solution

#### Request

```
GET /api/hangman/{id}/solution
```

#### Response

```
{
    "gameId": game_id,
    "solution": "hangman_word_solution"
}
```
