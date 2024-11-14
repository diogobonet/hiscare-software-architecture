const express = require('express');
const bodyParser = require('body-parser');
const axios = require('axios');
require('dotenv').config();

const app = express();

app.use(bodyParser.json());

// Create
app.post('/microservico/', (req, res) => {
    const url = `${process.env.API_USER_MS}`;

    axios.post(url, req.body)
      .then(response => {
        res.json(response.data);
      })
      .catch(error => {
        res.status(500).send(error.message);
      });
});

app.post('/function/', (req, res) => {
    const url = `${process.env.API_USER_FUNC_POST}`; 

    axios.post(url, req.body)
      .then(response => {
        res.json(response.data);
      })
      .catch(error => {
        res.status(500).send(error.message);
      });
});

// Read All and ID
app.get('/microservico/:id?', (req, res) => {
    const { id } = req.params; 
    let url = `${process.env.API_USER_MS};`

    if (id) {
      url = `${process.env.API_USER_MS}/${id}`
    }

    axios.get(url)
    .then(response => {
      res.json(response.data);
    })
    .catch(error => {
      res.status(500).send(error.message);
    });
});

app.get('/function/', (req, res) => {
  const crm = req.query.crm; 
  let url = `${process.env.API_USER_FUNC_GET}`; 

  if (crm) {
      url = `${process.env.API_USER_FUNC_GET}?crm=${crm}`;
  }

  axios.get(url)
    .then(response => {
      res.status(response.status).send(response.data);
    })
    .catch(error => {
      res.status(error.response ? error.response.status : 500).send(error.message);
    });
});

// Update
app.put('/microservico/:id', (req, res) => {
  const id = req.params.id;
  const url = `${process.env.API_USER_MS}/${id}`;

  axios.put(url, req.body)
    .then(response => {
      res.json(response.data);
    })
    .catch(error => {
      res.status(500).send(error.message);
    });
});

app.put('/function/', (req, res) => {
  const url = `${process.env.API_USER_FUNC_PUT}`; 

  axios.put(url, req.body)
  .then(response => {
    res.json(response.data);
  })
  .catch(error => {
    res.status(500).send(error.message);
  });
});

// Delete
app.delete('/microservico/:id', (req, res) => {
  const { id } = req.params;
  const url = `${process.env.API_USER_MS}/${id}`; 

  axios.delete(url)
  .then(response => {
    res.json(response.data);
  })
  .catch(error => {
    res.status(500).send(error.message);
  });
});

app.delete('/function/', (req, res) => {
  const crm = req.query.crm; 
  const url = `${process.env.API_USER_FUNC_DELETE}?crm=${crm}`;

  axios.delete(url)
    .then(response => {
      res.status(response.status).send(response.data);
    })
    .catch(error => {
      res.status(error.response ? error.response.status : 500).send(error.message);
    });
});

app.post('/sendCrm/', (req, res) => {

  const url = `${process.env.API_USER_MS_CRM}`; 

  axios.post(url, req.body)
      .then(response => {
          res.json(response.data);
          console.log(response.data);
      })
      .catch(error => {
          res.status(500).send(error.message);
          console.log(error.message);
      });
});

const PORT = 3000;
app.listen(PORT, () => {
    console.log(`Servidor rodando na porta ${PORT}`);
});
