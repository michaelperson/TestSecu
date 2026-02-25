# TestSecu - API de Démonstration Sécurisée (JWT & Validation)

Ce projet est une API ASP.NET Core (.NET 9) conçue comme support pédagogique pour les étudiants. Son objectif est d'illustrer la mise en place d'une authentification par **JSON Web Token (JWT)**, tout en mettant l'accent sur les bonnes pratiques de **sécurité**, la **sanitisation des entrées** et la **validation des modèles**.

---

## 🏗 Architecture du Projet

L'architecture choisie pour ce projet est **modulaire** afin de séparer les responsabilités, bien qu'elle ne suive pas strictement un standard du marché (comme la Clean Architecture).

-   **TestSecu** : Le point d'entrée (Web API). Contient les contrôleurs et les DTOs.
-   **TestSecu.Domain** : Contient les entités métier (`User.cs`) et les interfaces de repository.
-   **TestSecu.Infrastructure** : Implémente la logique d'accès aux données (simulée ici par une liste en mémoire dans `AccountRepository`).
-   **SecurityTools** : Une bibliothèque de classes partagée contenant les outils de sécurité réutilisables, notamment le `JwtHelper`.

---

## 🔐 Sécurité & Authentification JWT

### 1. Configuration (Program.cs)
L'authentification est configurée dans le pipeline middleware. Le serveur est configuré pour valider l'émetteur (*Issuer*), l'audience, la durée de vie (*Lifetime*) et la clé de signature du token.

### 2. Génération du Token (JwtHelper.cs)
Le [`JwtHelper`](file:///c:/Users/micha/source/repos/TestSecu/SecuirtyTools/JwtHelper.cs) centralise la création du token. 
> [!IMPORTANT]
> **Règle d'or des Claims** : N'ajoutez JAMAIS de données sensibles (mots de passe, numéros de sécurité sociale) dans les claims d'un JWT. Le contenu est encodé en Base64 et peut être facilement lu par n'importe qui possédant le token.

### 3. Utilisation dans le Contrôleur
L'[`AccountController`](file:///c:/Users/micha/source/repos/TestSecu/TestSecu/Controllers/AccountController.cs) gère l'appel à l'authentification et retourne le token généré au client.

---

## 🛡 Défense en Profondeur : Validation & Sanitisation

Le projet utilise plusieurs couches de protection pour garantir l'intégrité des données.

### Validation par Data Annotations (DTOs)
Dans le fichier [`UserDto.cs`](file:///c:/Users/micha/source/repos/TestSecu/TestSecu/dto/UserDto.cs), nous utilisons des attributs de validation pour rejeter les requêtes malformées dès l'entrée :
-   `[Required]` : Garantit que le champ n'est pas vide.
-   `[EmailAddress]` : Vérifie le format de l'email.
-   `[RegularExpression]` : Force une politique de mot de passe complexe (10+ caractères, Majuscule, Minuscule, Chiffre, Caractère spécial).

### Sanitisation & Validation au niveau Repository
Même si les données passent la validation du contrôleur, le [`AccountRepository`](file:///c:/Users/micha/source/repos/TestSecu/TestSecu.Infrastructure/Repositories/AccountRepository.cs) effectue une seconde vérification par **Regex** avant tout traitement. 
> [!TIP]
> Cette approche de "Double Validation" est cruciale pour se protéger contre les injections et s'assurer que les données traitées par la logique métier sont saines.

---

## 🧪 Tests des Modèles

Pour garantir que nos règles de sécurité sont toujours respectées, il est fortement conseillé de mettre en place des tests unitaires sur les DTOs et les modèles.

**Points clés à tester :**
1.  **Validité du Mot de Passe** : Tester des mots de passe trop courts, sans majuscule ou sans caractère spécial pour vérifier que la validation échoue.
2.  **Format d'Email** : S'assurer que les formats invalides sont bloqués.
3.  **Logique de Claims** : Vérifier que le `JwtHelper` génère bien les rôles attendus dans le token.

---

## 🚀 Installation & Utilisation

### Prérequis
-   SDK .NET 9.0
-   Visual Studio 2022 ou VS Code

### Lancement
1.  Clonez le repository.
2.  Restaurez les dépendances : `dotnet restore`.
3.  Lancez le projet : `dotnet run --project TestSecu`.
4.  L'API est documentée via **Scalar** (accessible en mode développement).

### Configuration JWT
Les paramètres du token se trouvent dans le fichier `appsettings.json` :
```json
"Jwt": {
  "SecretKey": "Une_Cle_Tres_Longue_Et_Tres_Securisee_123!",
  "Issuer": "TestSecuServer",
  "Audience": "TestSecuClient",
  "LifeTime": 60
}
```
