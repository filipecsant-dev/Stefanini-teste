const { env } = require('process');

const target = `https://localhost:7030`;

const PROXY_CONFIG = [
  {
    target: target,
    secure: false
  }
]

module.exports = PROXY_CONFIG;
