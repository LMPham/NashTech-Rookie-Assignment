import { AssetState } from "@features/assets/asset.type"

import { OptionItem } from "@/types/data"

export const createAssetStateOptions: OptionItem<AssetState>[] = [
  {
    label: "Available",
    value: AssetState.Available,
  },
  {
    label: "Not available",
    value: AssetState.NotAvailable,
  },
]

export const updateAssetStateOptions: OptionItem<AssetState>[] = [
  {
    label: "Available",
    value: AssetState.Available,
  },
  {
    label: "Not available",
    value: AssetState.NotAvailable,
  },
  {
    label: "Waiting for recycling",
    value: AssetState.WaitingForRecycling,
  },
  {
    label: "Recycled",
    value: AssetState.Recycled,
  },
]
