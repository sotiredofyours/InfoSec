import random

def extended_GCD(e, phi):
    d, x1, x2, y1 = 0, 0, 1, 1
    temp_phi = phi

    while e > 0:
        temp1 = temp_phi // e
        temp2 = temp_phi - temp1 * e
        temp_phi = e
        e = temp2

        x = x2 - temp1 * x1
        y = d - temp1 * y1
        x2 = x1
        x1 = x
        d = y1
        y1 = y

    if temp_phi == 1:
        return d + phi
        
def GCD(a, b):
    while b != 0:
        a, b = b, a % b
    return a

def is_Prime(num):
    if num < 2 or num % 2 == 0:
        return False
    for n in range(3, int(num ** 0.5) + 1):
        if num % n == 0:
            return False
    return True

def generate_keys(p, q):
    n = p * q
    phi = (p - 1) * (q - 1)
    e = random.randrange(1, phi)
    gcd = GCD(e, phi)

    while gcd != 1:
        e = random.randrange(1, phi)
        gcd = GCD(e, phi)

    d = extended_GCD(e, phi)
    return ((e, n), (d, n))

def encrypt(public_key, message):
    key, n = public_key
    encrypted_message = [(ord(char) ** key) % n for char in message]
    return encrypted_message

def decrypt(private_key, encrypted_message):
    key, n = private_key
    decrypted_message = [chr((char ** key) % n) for char in encrypted_message]
    return ''.join(decrypted_message)

if __name__ == '__main__':
    p = int(input("Enter first prime number: "))
    q = int(input("Enter second prime number: "))

    public_key, private_key = generate_keys(p, q)
    message = input("Message: ")
    encrypted_message = encrypt(public_key, message)

    print("Encrypted:", encrypted_message)

    decrypted_message = decrypt(private_key, encrypted_message)
    print("Decrypted:", decrypted_message)
