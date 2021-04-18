<template>
  <div class="container-fluid">
    <div class="card-columns ml-4 mr-4 mt-5">
      <keep-component v-for="keep in state.keeps" :key="keep.id" :keep-prop="keep" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import KeepComponent from '../components/KeepComponent.vue'
import { keepsService } from '../services/KeepsService'
export default {
  components: { KeepComponent },
  name: 'Home',
  setup() {
    const state = reactive({
      user: computed(() => AppState.user),
      keeps: computed(() => AppState.keeps)
    })
    onMounted(() => { keepsService.getAllKeeps() })
    return {
      state
    }
  }
}
</script>

<style scoped lang="scss">
.home{
  margin: 0;
  padding: 0;
  padding: 3% 0;;
}
</style>
