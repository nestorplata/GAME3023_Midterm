# GAME3023_Midterm
All Sprites were taken from Minecraft
As A reference I used the following videos and links as refernce:
https://www.youtube.com/watch?v=BGr-7GZJNXg
https://www.youtube.com/watch?v=E91NYvDqsy8&t=728sw
https://gamedev.stackexchange.com/questions/21586/how-could-i-implement-something-like-minecrafts-crafting-grid

A simple drag system helped me organize drag and drop events. Making when to check for recipes event based and not update based.
The I combined scriptable recipes and objects with the code example on the last link. Organized everytihng to utilizing char arrays as recipes.

In question of project organization:
The hyrerchy is as follow:
  UI_CraftManager
    UI_Crafter
    UI_Inventory
      ItemSlots
      ObjectInteractivit
Tried as much to reduce transform.GetComponent<Scripts> so must of the times I directly save the script instead of the gameObject

The last thing would be the use of Signifiers to differiantiate objects such as Tile type and Item Type(kinda)
    
