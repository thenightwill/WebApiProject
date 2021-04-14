# WebApiProject

List Of Request 
- Create Client
    Endpoint: /api/Client/createclient
    
    Type: POST
    
    Body:
    ```
  {
 
       "Credits":50000,
        "Identification" :"12565",
        "Names":"William",
        "LastNames" :"Gil"
  }
    ```

- Add Credits Client.

    Endpoint: /api/Client/addcredits
    
    Type: PUT
    
    Body: 
    ```
      {
        "IdClient" :"Client_ID",
        "Credits":50000
      }
    ```
      
- Create Roulette

    Endpoint: /api/Roulette/createroulette
    
    Type: GET

- Get all Roulettes

    Endpoint: /Roulette/getroulettes
    
    Type: GET
    
- Open Roulettes

    Endpoint: /Roulette/getroulettes
    
    Type: POST
    
    Body:
    ```
    {
       "RouletteID" :"RouletteID"  
    }
    ```
    
- Bet Roulettes

    Endpoint: /api/Roulette/betroulette
    
    Type: PUT
    
    Header: IdClient=ClientID
    
    Body:
    ```
   {
    "RouletteID" : "RouletteID",
    "BetColor":"rojo",
    "Betmoney" :10000
    }
    ```
    OR
    ```
    {
    "RouletteID" : "RouletteID",
    "BetNumber" : 23,
    "Betmoney" :10000
    }
    ```
    
 - Close Roulettes

    Endpoint: /api/Roulette/closeroulette
    
    Params: rouletteID=RouletteID
    
    Type: GET
