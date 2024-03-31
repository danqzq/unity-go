package main

type Player struct {
    Name string `json:"name"`
    Health int `json:"health"`
    Mana int `json:"mana"`
    Inventory []Item `json:"inventory"`
}

type Item struct {
    Name string `json:"name"`
    Type string `json:"type"`
    Quantity int `json:"quantity"`
}

type Enemy struct {
    Name string `json:"name"`
    Health int `json:"health"`
    Type string `json:"type"`
}