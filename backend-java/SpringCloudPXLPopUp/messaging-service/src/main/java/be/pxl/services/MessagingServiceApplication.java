package be.pxl.services;

import org.springframework.amqp.rabbit.annotation.RabbitListener;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.cloud.client.discovery.EnableDiscoveryClient;

/**
 * Hello world!
 *
 */

@SpringBootApplication
@EnableDiscoveryClient
public class MessagingServiceApplication {

    public static final String directExchangeName = "ordermanagement_event_bus";

    /**
     * We define our queue info
     *
     * @return Queue object
     */
//    @Bean
//    public Queue myQueue() {
//        return new Queue("myQueue", false);
//    }

    @RabbitListener(queues = "OrderPlacedIntegrationEvent")
    public void listen(String in) {

        System.out.println("Message read from myQueue : " + in);
    }

//
//    @Bean
//    DirectExchange exchange() {
//        return new DirectExchange(directExchangeName);
//    }

//    @Bean
//    Binding binding(Queue queue, DirectExchange exchange) {
//        return BindingBuilder.bind((Exchange) queue).to(exchange).with("BarReceivedIntegrationEvent");
//    }


    public static void main(String[] args) {
        SpringApplication.run(MessagingServiceApplication.class, args);
    }



}
