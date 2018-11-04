import Vue from 'vue'
import axios from 'axios'
import App from 'components/app-root'
import { FontAwesomeIcon } from './icons'
import moment from 'moment'

// Registration of global components
Vue.component('icon', FontAwesomeIcon)

Vue.filter('formatDate', function (value) {
  if (value) {
    return moment(String(value)).format('DD.MM.YYYY HH:mm:ss')
  }
})



Vue.prototype.$http = axios

const app = new Vue({
  ...App
})

export {
  app
}
