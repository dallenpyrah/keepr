import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  account: {},
  keeps: [],
  activeKeep: {},
  vaults: [],
  activeVault: {},
  profile: {},
  profileKeeps: [],
  profileVaults: [],
  profileVaultKeeps: [],
  vaultKeeps: [],
  isLoading: true,
  vaultPage: false
})
