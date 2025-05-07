FROM nginx:1.27.5-alpine
COPY dist /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
CMD ["/bin/sh", "-c", "envsubst < /usr/share/nginx/html/config/env.template.js > /usr/share/nginx/html/config/env.js && nginx -g 'daemon off;'"]