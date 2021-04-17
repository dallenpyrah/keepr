import { AppState } from '../AppState'
import { api } from './AxiosService'

class VaultKeepsService {
  async createVaultKeep(newVaultKeep) {
    const res = await api.post('api/vaultkeeps', newVaultKeep)
    AppState.profileVaultKeeps = [...AppState.profileVaultKeeps, res.data]
  }
}

export const vaultKeepsService = new VaultKeepsService()
