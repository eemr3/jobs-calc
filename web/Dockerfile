FROM node:20-alpine

WORKDIR /app

# Copiar e instalar dependências
COPY package.json package-lock.json ./
RUN npm install --frozen-lockfile

# Copiar o restante dos arquivos
COPY . .

EXPOSE 3000
CMD ["npm", "run", "dev"]
