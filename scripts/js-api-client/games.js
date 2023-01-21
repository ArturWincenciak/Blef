const Url = 'https://localhost:49153/games-module/games';
const Request = {
    headers: {
        "content-type": "application/json; charset=UTF-8"
    },
    method: "POST"
}

const diplayResponse = data => {
   if(!data) {
       console.error("There is no data");
       return;
   }

   console.info("Response: ", data);
   document.getElementById('gameId').innerHTML = data.gameId;
}

fetch(Url, Request)
    .then((response) => response.json())
    .then((response) => diplayResponse(response));