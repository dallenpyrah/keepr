<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-success">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center">
        <img
          class="old-hover"
          alt="logo"
          src="../assets/img/K.png"
          height="50"
        />
      </div>
    </router-link>
    <button
      class="navbar-toggler"
      type="button"
      data-toggle="collapse"
      data-target="#navbarText"
      aria-controls="navbarText"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <span class="navbar-toggler-icon" />
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <div class="col-4 mr-auto ml-auto">
        <form @submit.prevent="filterResult">
          <div class="form-group mt-3">
            <div class="input-group">
              <input type="text"
                     class="form-control form-control-lg border-right-0"
                     placeholder="From"
                     aria-label="from"
                     aria-describedby="from"
                     v-model="state.tagQuery"
              >
              <div class="input-group-append">
                <span class="input-group-text bg-white"><i class="fa fa-search" aria-hidden="true"></i></span>
              </div>
            </div>
          </div>
        </form>
      </div>
      <span class="card p-2 bg-dark text-light rounded">
        <button
          class="btn btn-outline-dark text-light text-uppercase"
          @click="login"
          v-if="!user.isAuthenticated"
        >
          Login
        </button>

        <div class="dropdown" v-else>
          <div
            class="dropdown-toggle"
            @click="state.dropOpen = !state.dropOpen"
          >
            <img
              :src="user.picture"
              alt="user photo"
              height="40"
              class="rounded"
            />
            <span class="mx-3">{{ user.name }}</span>
          </div>
          <div
            class="dropdown-menu p-0 list-group w-100"
            :class="{ show: state.dropOpen }"
            @click="state.dropOpen = false"
          >
            <router-link :to="{ name: 'Account', params: { id: state.account.id} }">
              <div class="list-group-item list-group-item-action hoverable">
                Account
              </div>
            </router-link>
            <div
              class="list-group-item list-group-item-action hoverable"
              @click="logout"
            >
              logout
            </div>
          </div>
        </div>
      </span>
    </div>
  </nav>
</template>

<script>
import { AuthService } from '../services/AuthService'
import { AppState } from '../AppState'
import { computed, reactive } from 'vue'
import { keepsService } from '../services/KeepsService'
export default {
  name: 'Navbar',
  setup() {
    const state = reactive({
      account: computed(() => AppState.account),
      dropOpen: false,
      tagQuery: ''
    })
    return {
      state,
      user: computed(() => AppState.user),
      async login() {
        AuthService.loginWithPopup()
      },
      async logout() {
        await AuthService.logout({ returnTo: window.location.origin })
      },
      async filterResult() {
        await keepsService.filterKeeps()
      }
      // async filterResult() {
      //   console.log('Hey')
      //   AppState.keeps = AppState.keeps.filter(k => k.tags === state.tagQuery)
      //   if (state.tagQuery === '' || null) {
      //     await keepsService.getAllKeeps()
      //   }
      // }
    }
  }
}
</script>

<style scoped>
.dropdown-menu {
  user-select: none;
  display: block;
  transform: scale(0);
  transition: all 0.15s linear;
}
.dropdown-menu.show {
  transform: scale(1);
}
.hoverable {
  cursor: pointer;
}
a:hover {
  text-decoration: none;
}
.nav-link{
  text-transform: uppercase;
}
.nav-item .nav-link.router-link-exact-active{
  color: var(--dark);
}
</style>
