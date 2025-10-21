package student.projects.leaselinkapplication.ManagerLanding

import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import student.projects.leaselinkapplication.R

class SendBroadcastActivity : AppCompatActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_send_broadcast)

        val sendToInput = findViewById<EditText>(R.id.addressEdit2)
        val subjectInput = findViewById<EditText>(R.id.addressEdit3)
        val messageInput = findViewById<EditText>(R.id.addressEdit4)
        val attachmentInput = findViewById<EditText>(R.id.addressEdit6)
        val sendButton = findViewById<Button>(R.id.button21)

        sendButton.setOnClickListener {
            val sendTo = sendToInput.text.toString().trim()
            val subject = subjectInput.text.toString().trim()
            val message = messageInput.text.toString().trim()
            val attachment = attachmentInput.text.toString().trim()

            if (sendTo.isEmpty() || subject.isEmpty() || message.isEmpty()) {
                Toast.makeText(this, "Please fill in all required fields", Toast.LENGTH_SHORT).show()
            } else {
                Toast.makeText(
                    this,
                    "Broadcast Sent:\nTo: $sendTo\nSubject: $subject\nMessage: $message\nAttachment: $attachment",
                    Toast.LENGTH_LONG
                ).show()
            }
        }
    }
}